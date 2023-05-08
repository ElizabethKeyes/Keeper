import { AppState } from "../AppState.js"
import { VaultKeep } from "../models/VaultKeep.js"
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
}

export const vaultKeepsService = new VaultKeepsService();