import { Component, EventEmitter, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TodoService } from '../../services/todo.service';
import { CreateTodoRequest } from '../../models/todo.model';

@Component({
  selector: 'app-add-todo-modal',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './add-todo-modal.component.html'
})
export class AddTodoModalComponent {
  @Output() todoAdded = new EventEmitter<void>();
  @Output() modalClosed = new EventEmitter<void>();

  todo: CreateTodoRequest = {
    title: '',
    description: ''
  };

  loading = false;
  error = '';

  constructor(private todoService: TodoService) {}

  onSubmit(): void {
    if (!this.todo.title.trim()) {
      this.error = 'Title is required';
      return;
    }

    this.loading = true;
    this.error = '';

    this.todoService.createTodo(this.todo).subscribe({
      next: (createdTodo) => {
        this.loading = false;
        this.todoAdded.emit();
        this.resetForm();
      },
      error: (error) => {
        this.loading = false;
        this.error = error.message || 'Error creating todo. Please try again.';
      }
    });
  }

  onCancel(): void {
    this.modalClosed.emit();
    this.resetForm();
  }

  private resetForm(): void {
    this.todo = {
      title: '',
      description: ''
    };
    this.error = '';
  }
} 