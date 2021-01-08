import { logger } from '../utils/Logger'
import { api } from './AxiosService'
import { AppState } from '../AppState'
class KeepService {
  async getKeeps() {
    try {
      const res = await api.get('api/keeps')
      // logger.log(res.data)
      AppState.keeps = res.data
    } catch (error) {
      logger.error(error)
    }
  }

  async getMyKeeps() {
    try {
      // logger.log(AppState.profile)
      const res = await api(`api/profiles/${AppState.profile.id}/keeps`)
      AppState.myKeeps = res.data
    } catch (error) {
      logger.error(error)
    }
  }

  async getVisitedKeeps(id) {
    try {
      // logger.log(AppState.profile)
      const res = await api(`api/profiles/${id}/keeps`)
      AppState.myKeeps = res.data
    } catch (error) {
      logger.error(error)
    }
  }

  async getVaultKeeps(vaultId) {
    try {
      // logger.log(AppState.profile)
      const res = await api(`api/vaults/${vaultId}/keeps`)
      AppState.vaultKeeps = res.data
    } catch (error) {
      logger.error(error)
    }
  }

  async create(keep) {
    try {
      await api.post('api/keeps', keep)
      this.getKeeps()
      this.getMyKeeps()
    } catch (error) {
      logger.log(error)
    }
  }

  async delete(keepId) {
    try {
      await api.delete('api/keeps/' + keepId)
      this.getKeeps()
      this.getMyKeeps()
    } catch (error) {
      logger.log(error)
    }
  }

  async edit(keepId, keep) {
    try {
      await api.put(`api/keeps/${keepId}`, keep)
    } catch (error) {
      logger.log(error)
    }
  }
}

export const keepService = new KeepService()
