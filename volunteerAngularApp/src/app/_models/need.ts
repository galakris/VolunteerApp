import { NeedState } from './need-state';
import { NeedCategory } from './need-category';


export class Need {
  id: number;
  userId: number;
  category: NeedCategory;
  state: NeedState;
  description: string;
  distance?: number;
}
