<script setup>
import { ref, onMounted } from 'vue'
import { useTodoStore } from '@/stores/todo'

const store = useTodoStore()
const newTodo = ref('')

onMounted(() => {
  store.fetchTodos()
})

const addTodo = () => {
  if (newTodo.value.trim()) {
    store.addTodo(newTodo.value.trim())
    newTodo.value = ''
  }
}
</script>

<template>
  <div class="app-wrapper">
    <div class="app-content">
      <header class="app-header">
        <h1>Todo App</h1>
        <p>Get things done</p>
      </header>

      <div class="input-container">
        <input
          v-model="newTodo"
          @keyup.enter="addTodo"
          placeholder="What needs to be done?"
        >
        <button @click="addTodo">Add</button>
      </div>

      <div v-if="store.loading" class="loading-spinner">
        <div class="spinner"></div>
      </div>

      <div v-else class="todo-container">
        <div 
          v-for="todo in store.todos"
          :key="todo.id"
          class="todo-item"
        >
          <input
            type="checkbox"
            :checked="todo.isCompleted"
            @change="store.toggleTodo(todo.id)"
          >
          <span :class="{ completed: todo.isCompleted }">{{ todo.title }}</span>
          <button @click="store.deleteTodo(todo.id)">×</button>
        </div>

        <div v-if="!store.todos.length" class="empty-state">
          No tasks yet. Add your first task!
        </div>
      </div>

      <div v-if="store.todos.length" class="stats">
        <span>{{ store.todos.filter(t => !t.isCompleted).length }} left</span>
        <span>{{ store.todos.filter(t => t.isCompleted).length }} done</span>
      </div>
    </div>
  </div>
</template>

<style>
/* Reset and base styles */
* {
  margin: 0;
  padding: 0;
  box-sizing: border-box;
  font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Oxygen, Ubuntu, Cantarell, sans-serif;
}

/* Full screen layout */
.app-wrapper {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  display: flex;
  justify-content: center;
  align-items: center;
  background-color: #f8f9fa;
  overflow: auto;
}

.app-content {
  width: 100%;
  max-width: 600px;
  padding: 2rem;
  display: flex;
  flex-direction: column;
  gap: 1.5rem;
}

/* Header styles */
.app-header {
  text-align: center;
  margin-bottom: 1rem;
}

.app-header h1 {
  font-size: 2.5rem;
  font-weight: 300;
  color: #333;
  margin-bottom: 0.5rem;
}

.app-header p {
  color: #6c757d;
  font-size: 1rem;
}

/* Input styles */
.input-container {
  display: flex;
  width: 100%;
}

.input-container input {
  flex: 1;
  padding: 0.75rem 1rem;
  border: 1px solid #ced4da;
  border-radius: 0.25rem 0 0 0.25rem;
  font-size: 1rem;
  outline: none;
}

.input-container button {
  padding: 0 1.5rem;
  background-color: #4285f4;
  color: white;
  border: none;
  border-radius: 0 0.25rem 0.25rem 0;
  cursor: pointer;
  transition: background-color 0.2s;
}

.input-container button:hover {
  background-color: #3367d6;
}

/* Todo list styles */
.todo-container {
  width: 100%;
  display: flex;
  flex-direction: column;
  gap: 0.5rem;
}

.todo-item {
  display: flex;
  align-items: center;
  padding: 0.75rem 1rem;
  background-color: white;
  border-radius: 0.25rem;
  box-shadow: 0 1px 3px rgba(0,0,0,0.1);
  gap: 1rem;
}

.todo-item input[type="checkbox"] {
  width: 1.25rem;
  height: 1.25rem;
  cursor: pointer;
}

.todo-item span {
  flex: 1;
  font-size: 1.1rem;
}

.todo-item span.completed {
  text-decoration: line-through;
  color: #6c757d;
}

.todo-item button {
  background: none;
  border: none;
  font-size: 1.5rem;
  color: #adb5bd;
  cursor: pointer;
  padding: 0 0.5rem;
  line-height: 1;
}

.todo-item button:hover {
  color: #dc3545;
}

/* Empty state */
.empty-state {
  text-align: center;
  color: #6c757d;
  padding: 2rem;
  font-style: italic;
}

/* Loading spinner */
.loading-spinner {
  display: flex;
  justify-content: center;
  padding: 2rem;
}

.spinner {
  width: 2.5rem;
  height: 2.5rem;
  border: 0.25rem solid rgba(66, 133, 244, 0.2);
  border-top-color: #4285f4;
  border-radius: 50%;
  animation: spin 1s linear infinite;
}

/* Stats */
.stats {
  display: flex;
  justify-content: space-between;
  color: #6c757d;
  font-size: 0.9rem;
  padding-top: 1rem;
  border-top: 1px solid #e9ecef;
}

/* Animation */
@keyframes spin {
  to { transform: rotate(360deg); }
}
</style>