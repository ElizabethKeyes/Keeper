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
    if (AppState.vaultKeepDictionary[keepId]) {
      AppState.vaultKeepDictionary[keepId].push(vaultId)
    } else if (!AppState.vaultKeepDictionary[keepId]) {
      AppState.vaultKeepDictionary[keepId] = []
      AppState.vaultKeepDictionary[keepId].push(vaultId)
    }
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

  async getMyVaultKeeps() {
    const res = await api.get(`account/vaultkeeps`)
    AppState.myVaultKeeps = res.data.map(vk => new VaultKeep(vk))

    // sorting vaultKeeps into dictionary so I can refer to the IDs when populating dropdown menu
    AppState.myVaultKeeps.forEach(k => {
      if (AppState.vaultKeepDictionary[k.keepId]) {
        AppState.vaultKeepDictionary[k.keepId].push(k.vaultId)
      } else if (!AppState.vaultKeepDictionary[k.keepId]) {
        AppState.vaultKeepDictionary[k.keepId] = []
        AppState.vaultKeepDictionary[k.keepId].push(k.vaultId)
      }
    })
  }
}

export const vaultKeepsService = new VaultKeepsService();