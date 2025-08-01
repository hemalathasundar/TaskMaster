import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, map, catchError, throwError } from 'rxjs';
import { Todo, CreateTodoRequest } from '../models/todo.model';
import { environment } from '../../environments/environment';

interface ApiResponse {
  todos: Todo[];
}

@Injectable({
  providedIn: 'root'
})
export class TodoService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  private handleError(error: HttpErrorResponse) {
    let errorMessage = 'An error occurred';
    
    if (error.error instanceof ErrorEvent) {
      errorMessage = `Client Error: ${error.error.message}`;
    } else {
      errorMessage = `Server Error: ${error.status} - ${error.message}`;
    }
    
    return throwError(() => new Error(errorMessage));
  }

  getAllTodos(): Observable<Todo[]> {
    return this.http.get<ApiResponse>(this.apiUrl).pipe(
      map(response => response.todos || []),
      catchError(this.handleError)
    );
  }

  createTodo(todo: CreateTodoRequest): Observable<Todo> {
    return this.http.post<Todo>(this.apiUrl, todo).pipe(
      catchError(this.handleError)
    );
  }

  deleteTodo(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      catchError(this.handleError)
    );
  }
} 