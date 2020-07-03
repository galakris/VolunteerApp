import { NeedState } from './need-state';
import { NeedCategory } from './need-category';


export class Need {
  id: number;
  name: string;
  userId: number;
  category: NeedCategory;
  state: NeedState;
  description: string;
  deadlineDate: Date;
  lat?: number;
  lng?: number;
  distance?: number;
}
