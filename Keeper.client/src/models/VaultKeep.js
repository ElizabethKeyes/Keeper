import { Profile } from "./Account.js"
import { Keep } from "./Keep.js"

export class VaultKeep {
  constructor(data) {
    this.id = data.id
    this.vaultId = data.vaultId
    this.keepId = data.keepId
    this.creatorId = data.creatorId
    this.keep = data.keep ? new Keep(data.keep) : null
    this.creator = data.creator ? new Profile(data.creator) : null
  }
}

export class KeepInVault extends Keep {
  constructor(data) {
    super(data)
    this.vaultKeepId = data.vaultKeepId
  }
}