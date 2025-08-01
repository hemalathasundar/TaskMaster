export interface Todo {
  id: string;
  title: string;
  description: string;
  createdAt: Date;
}

export interface CreateTodoRequest {
  title: string;
  description: string;
} 