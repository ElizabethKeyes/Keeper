import { AppState } from "../AppState.js";
import { Vault } from "../models/Vault.js";
import { VaultKeep } from "../models/VaultKeep.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class VaultsService {
  async GetMyVaults(userId) {
    const res = await api.get(`api/profiles/${userId}/vaults`)
    AppState.myVaults = res.data.map(v => new Vault(v))
  }

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

export const vaultsService = new VaultsService();