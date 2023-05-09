import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  /** @type {import('./models/Account.js').Account} */
  account: {},
  /** @type {import('./models/Keep.js').Keep[]} */
  keeps: [],
  /** @type {import('./models/Keep.js').Keep[]} */
  profileKeeps: [],
  /** @type {import('./models/Keep.js').Keep|null} */
  activeKeep: null,
  /** @type {import('./models/Vault.js').Vault[]} */
  myVaults: [],
  /** @type {import('./models/Vault.js').Vault[]} */
  profileVaults: [],
  /** @type {import('./models/Vault.js').Vault|null} */
  activeVault: null,
  /** @type {import('./models/VaultKeep.js').VaultKeep|null} */
  vaultKeep: null,
  /** @type {import('./models/VaultKeep.js').KeepInVault[]} */
  keepsInVault: [],
  /** @type {import('./models/Profile.js').Profile|null} */
  activeProfile: null,

})
