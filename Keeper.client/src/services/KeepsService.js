import { Modal } from "bootstrap";
import { AppState } from "../AppState.js";
import { Keep } from "../models/Keep.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class KeepsService {
  async GetAllKeeps() {
    const res = await api.get(`api/keeps`)
    AppState.keeps = res.data.map(k => new Keep(k))
  }

  async setActiveKeep(keepId) {
    const res = await api.get(`api/keeps/${keepId}`)
    AppState.activeKeep = new Keep(res.data)
  }

  async createKeep(keepData) {
    const res = await api.post(`api/keeps`, keepData)
    AppState.keeps.push(new Keep(res.data))
    window.scrollTo({ left: 0, top: document.body.scrollHeight, behavior: "smooth" })
    Modal.getOrCreateInstance("#createKeepModal").hide()
  }
}

export const keepsService = new KeepsService();