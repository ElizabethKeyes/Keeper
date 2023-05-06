import { Profile } from "./Account.js"

export class Keep {
  constructor(data) {
    this.id = data.id
    this.creatorId = data.creatorId
    this.name = data.name
    this.description = data.description
    this.img = data.img
    this.views = data.views
    this.creator = new Profile(data.creator)
    this.creator = data.creator
    this.kept = data.kept
  }
}