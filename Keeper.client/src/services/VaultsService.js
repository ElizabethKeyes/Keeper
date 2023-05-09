import { AppState } from "../AppState.js";
import { Vault } from "../models/Vault.js";
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
  }

  async setActiveVault(vaultId) {
    const res = await api.get(`api/vaults/${vaultId}`)
    AppState.activeVault = new Vault(res.data)
  }

  async getVaultsByProfileId(profileId) {
    const res = await api.get(`api/profiles/${profileId}/vaults`)
    AppState.profileVaults = res.data.map(v => new Vault(v))
  }

  async deleteVault(vaultId) {
    const res = await api.delete(`api/vaults/${vaultId}`)
  }
}

export const vaultsService = new VaultsService();