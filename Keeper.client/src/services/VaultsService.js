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

  async createVault(vaultData) {
    const res = await api.post(`api/vaults`, vaultData)
    AppState.myVaults.push(new Vault(res.data))
    logger.log(res.data)
  }
}

export const vaultsService = new VaultsService();