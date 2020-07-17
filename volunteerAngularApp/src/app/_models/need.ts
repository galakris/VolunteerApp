import { NeedState } from './need-state';
import { NeedCategory } from './need-category';


export class Need {
  id: number;
  name: string;
  userId: number;
  category: NeedCategory;
  needStatus: NeedState;
  description: string;
  deadlineDate: Date;
  latitude?: number;
  longitude?: number;
  distance?: number;
}
