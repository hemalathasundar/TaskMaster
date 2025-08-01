import { Component, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AgGridAngular, AgGridModule } from 'ag-grid-angular';
import { ColDef, GridReadyEvent, ICellRendererParams, GridApi } from 'ag-grid-community';
import { TodoService } from '../../services/todo.service';
import { Todo } from '../../models/todo.model';
import { AddTodoModalComponent } from '../add-todo-modal/add-todo-modal.component';

@Component({
  selector: 'app-todo-list',
  standalone: true,
  imports: [CommonModule, FormsModule, AgGridModule, AddTodoModalComponent],
  templateUrl: './todo-list.component.html'
})
export class TodoListComponent implements OnInit {
  @ViewChild(AgGridAngular) agGrid!: AgGridAngular;

  todos: Todo[] = [];
  showAddModal = false;
  loading = false;
  gridApi!: GridApi;

  columnDefs: ColDef[] = [
    { 
      field: 'title', 
      headerName: 'Title',
      width: 200,
      sortable: true,
      filter: true
    },
    { 
      field: 'description', 
      headerName: 'Description',
      width: 300,
      sortable: true,
      filter: true
    },
    { 
      field: 'createdAt', 
      headerName: 'Created',
      width: 150,
      sortable: true,
      filter: true,
      valueFormatter: (params) => {
        if (!params.value) return '';
        return new Date(params.value).toLocaleDateString();
      }
    },
    {
      headerName: 'Actions',
      width: 120,
      sortable: false,
      filter: false,
      cellRenderer: (params: ICellRendererParams) => {
        return `
          <button class="btn btn-danger btn-sm" 
                  onclick="window.deleteTodo('${params.data.id}')"
                  title="Delete this task">
            <i class="fas fa-trash"></i>
          </button>
        `;
      }
    }
  ];

  defaultColDef: ColDef = {
    sortable: true,
    filter: true
  };

  constructor(private todoService: TodoService) {
    (window as any).deleteTodo = (id: string) => this.deleteTodo(id);
  }

  ngOnInit(): void {
    this.loadTodos();
  }

  onGridReady(params: GridReadyEvent): void {
    this.gridApi = params.api;
    params.api.sizeColumnsToFit();
  }

  loadTodos(): void {
    this.loading = true;
    this.todoService.getAllTodos().subscribe({
      next: (todos) => {
        this.todos = todos || [];
        this.loading = false;
        if (this.gridApi) {
          this.gridApi.setRowData(this.todos);
        }
      },
      error: (error) => {
        this.todos = [];
        this.loading = false;
        if (this.gridApi) {
          this.gridApi.setRowData([]);
        }
      }
    });
  }

  openAddModal(): void {
    this.showAddModal = true;
  }

  closeAddModal(): void {
    this.showAddModal = false;
  }

  onTodoAdded(): void {
    this.closeAddModal();
    this.loadTodos();
  }

  deleteTodo(id: string): void {
    if (confirm('Are you sure you want to delete this todo?')) {
      this.todoService.deleteTodo(id).subscribe({
        next: () => {
          this.loadTodos();
        },
        error: (error) => {
          alert('Error deleting todo. Please try again.');
        }
      });
    }
  }
} 