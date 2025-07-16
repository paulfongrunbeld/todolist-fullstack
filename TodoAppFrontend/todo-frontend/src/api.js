import axios from 'axios'

const api = axios.create({
  baseURL: 'http://localhost:5130/api' // Ваш бекенд
})

export default {
  async getTodos() {
    const res = await api.get('/todo')
    return res.data
  },
  async addTodo(title) {
    const res = await api.post('/todo', { title })
    return res.data
  },
  async updateTodo(id, updates) {
    await api.put(`/todo/${id}`, updates)
  },
  async deleteTodo(id) {
    await api.delete(`/todo/${id}`)
  }
}