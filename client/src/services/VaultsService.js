import { logger } from '../utils/Logger'
import { api } from './AxiosService'
import { AppState } from '../AppState'
class VaultsService {
  async getVaults() {
    try {
      const res = await api.get('api/vaults')
      logger.log(res.data)
      AppState.vaults = res.data
    } catch (error) {
      logger.error(error)
    }
  }

  async getVaultById(id) {
    try {
      const res = await api.get('api/vaults/' + id)
      logger.log(id)
      // logger.log(res.data)
      AppState.activeVault = res.data
    } catch (error) {
      logger.error(error)
    }
  }

  async getMyVaults() {
    try {
      logger.log(AppState.profile)
      const res = await api(`profiles/${AppState.profile.id}/vaults`)
      AppState.vaults = res.data
    } catch (error) {
      logger.error(error)
    }
  }

  async getVaultKeeps(vaultId) {
    try {
      logger.log(AppState.profile)
      const res = await api(`api/vaults/${vaultId}/keeps`)
      AppState.vaultKeeps = res.data
    } catch (error) {
      logger.error(error)
    }
  }

  async createVaultKeep(vaultKeep) {
    try {
      await api.post('api/vaultkeeps', vaultKeep)
      // this.getKeeps()
      // NOTE add edit to increase keep count
    } catch (error) {
      logger.log(error)
    }
  }

  async create(vault) {
    try {
      await api.post('api/vaults', vault)
      this.getMyVaults()
    } catch (error) {
      logger.log(error)
    }
  }

  async delete(vaultId) {
    try {
      await api.delete('api/vaultkeeps/' + vaultId + '/keeps')
      await api.delete(`api/vaults/${vaultId}`)
    } catch (error) {
      logger.log(error)
    }
  }

  async removeFromVault(vaultId, vk) {
    try {
      await api.delete('api/vaultkeeps/' + vk.vaultKeepId)
      this.getVaultKeeps(vaultId)
    } catch (error) {
      logger.log(error)
    }
  }

  async edit(vaultId, vault) {
    try {
      await api.put(`api/vaults/${vaultId}`)
    } catch (error) {
      logger.log(error)
    }
  }
}

export const vaultsService = new VaultsService()
