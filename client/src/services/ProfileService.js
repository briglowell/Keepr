import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class ProfileService {
  async getProfile() {
    try {
      const res = await api.get('api/profiles')
      AppState.profile = res.data
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }

  async clearAppstate() {
    AppState.activeVault = {}
    AppState.keeps = []
    AppState.vaultKeeps = []
    AppState.vaults = []
    AppState.profile = {}
  }
}

export const profileService = new ProfileService()
