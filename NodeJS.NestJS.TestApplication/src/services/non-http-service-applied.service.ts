import { Injectable } from '@nestjs/common';
import { IntentIgnoreBody, IntentIgnore } from './../intent/intent.decorators';

@Injectable()
export class NonHttpServiceAppliedService {

  //@IntentCanAdd()
  constructor() {}

  @IntentIgnoreBody()
  async operation1(): Promise<void> {
    throw new Error("Write your implementation for this service here...");
  }
}
