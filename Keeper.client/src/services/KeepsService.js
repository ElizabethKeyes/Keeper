import { Modal } from "bootstrap";
import { AppState } from "../AppState.js";
import { Keep } from "../models/Keep.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";
import Pop from "../utils/Pop.js";

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

  async deleteKeep(keepId) {
    const res = await api.delete(`api/keeps/${keepId}`)
    Modal.getOrCreateInstance("#activeKeepModal").hide()
    const foundIndex = AppState.keeps.findIndex(k => k.id == keepId)
    AppState.keeps.splice(foundIndex, 1)
    if (AppState.keepsInVault.length > 0) {
      const foundIndex2 = AppState.keepsInVault.findIndex(k => k.id == keepId)
      AppState.keepsInVault.splice(foundIndex2, 1)
    }
  }

  async getKeepsByProfileId(profileId) {
    const res = await api.get(`api/profiles/${profileId}/keeps`)
    AppState.profileKeeps = res.data.map(k => new Keep(k))
  }
}

export const keepsService = new KeepsService();