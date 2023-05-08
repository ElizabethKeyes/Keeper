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
    logger.log('[NEW VAULTKEEP]', AppState.vaultKeep)
  }

  async getKeepsInVault(vaultId) {
    const res = await api.get(`api/vaults/${vaultId}/keeps`)
    AppState.keepsInVault = res.data.map(vk => new KeepInVault(vk))
  }
}

export const vaultKeepsService = new VaultKeepsService();