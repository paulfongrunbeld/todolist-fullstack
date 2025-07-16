import { defineStore } from 'pinia'
import api from '@/api'

export const useTodoStore = defineStore('todos', {
  state: () => ({
    todos: [],
    loading: false
  }),
  actions: {
    async fetchTodos() {
      this.loading = true
      this.todos = await api.getTodos()
      this.loading = false
    },
    async addTodo(title) {
      const todo = await api.addTodo(title)
      this.todos.push(todo)
    },
    async toggleTodo(id) {
      const todo = this.todos.find(t => t.id === id)
      if (todo) {
        const updated = { ...todo, isCompleted: !todo.isCompleted }
        await api.updateTodo(id, updated)
        await this.fetchTodos()
      }
    },
    async deleteTodo(id) {
      await api.deleteTodo(id)
      this.todos = this.todos.filter(t => t.id !== id)
    }
  }
})