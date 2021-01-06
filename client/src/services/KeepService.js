import { logger } from '../utils/Logger'
import { api } from './AxiosService'
import { AppState } from '../AppState'
class KeepService {
  async getKeeps() {
    try {
      const res = await api.get('api/keeps')
      logger.log(res.data)
      AppState.blogs = res.data
    } catch (error) {
      logger.error(error)
    }
  }

  async getVaultKeeps(vaultId) {
    try {
      logger.log(AppState.profile)
      const res = await api(`api/vaults/${vaultId}/vaultkeeps`)
      AppState.vaultKeeps = res.data
      // NOTE dont forget to add your js doc types in the appstate
      // AppState.blogs = res.data.map(e => new Blog(e.title, e.body))
      // this now has intellisense on 'b' AppState.blogs.forEach(b=> b.)
    } catch (error) {
      logger.error(error)
    }
  }

  async create(keep) {
    try {
      await api.post('api/keeps', keep)
      this.getKeeps()
    } catch (error) {
      logger.log(error)
    }
  }
}

export const keepService = new KeepService()
