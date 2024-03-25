import { Photo } from "./photo"

export interface Newfeed {
    id: number
    creatorId: number
    creatorUserName: string
    creatorPhotoUrl: string
    content: string
    photos: Photo[]
    feeling:string
    postedTime: Date;
    lastModifiedTime: Date;

  }
  