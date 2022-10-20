import { Injectable } from '@nestjs/common';
import { IntentIgnoreBody, IntentIgnore } from './../intent/intent.decorators';

@Injectable()
export class HttpServiceAppliedService {

  //@IntentCanAdd()
  constructor() {}

  @IntentIgnoreBody()
  async getValue(): Promise<string> {
    throw new Error("Write your implementation for this service here...");
  }

  @IntentIgnoreBody()
  async postValue(value: string): Promise<void> {
    throw new Error("Write your implementation for this service here...");
  }

  @IntentIgnoreBody()
  async nonAppliedOperation(): Promise<void> {
    throw new Error("Write your implementation for this service here...");
  }
}
