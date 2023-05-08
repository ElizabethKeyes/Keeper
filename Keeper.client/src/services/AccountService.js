import { Modal } from "bootstrap"
import { AppState } from '../AppState'
import { Account } from '../models/Account.js'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'
import Pop from "../utils/Pop.js"

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = new Account(res.data)
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }

  async editAccount(accountData) {
    const res = await api.put(`/account`, accountData)
    AppState.account = new Account(res.data)
    Modal.getOrCreateInstance("#editProfileModal").hide()
    Pop.toast("Your changes have been saved", "success", "top")
  }
}

export const accountService = new AccountService()
