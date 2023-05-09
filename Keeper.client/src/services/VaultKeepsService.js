import { Modal } from "bootstrap"
import { AppState } from "../AppState.js"
import { KeepInVault, VaultKeep } from "../models/VaultKeep.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class VaultKeepsService {
  async addToVault(vaultId, keepId) {
    const vaultKeepData = {
      'vaultId': vaultId,
      'keepId': keepId
    }
    const res = await api.post(`api/vaultkeeps`, vaultKeepData)
    AppState.vaultKeep = new VaultKeep(res.data)
    AppState.activeKeep.kept++
  }

  async getKeepsInVault(vaultId) {
    const res = await api.get(`api/vaults/${vaultId}/keeps`)
    AppState.keepsInVault = res.data.map(vk => new KeepInVault(vk))
  }

  async removeKeepFromVault(vaultKeepId, keepId) {
    const res = await api.delete(`api/vaultkeeps/${vaultKeepId}`)
    const foundIndex = AppState.keeps.findIndex(k => k.id == keepId)
    AppState.keeps.splice(foundIndex, 1)
    const foundIndex2 = AppState.keepsInVault.findIndex(k => k.vaultKeepId == vaultKeepId)
    AppState.keepsInVault.splice(foundIndex2, 1)
    Modal.getOrCreateInstance("#activeKeepModal").hide()
  }
}

export const vaultKeepsService = new VaultKeepsService();