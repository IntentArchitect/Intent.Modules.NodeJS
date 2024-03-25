import { Injectable } from '@nestjs/common';
import { IntentIgnoreBody } from './../intent/intent.decorators';

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

  @IntentIgnoreBody()
  async getNumberArray(): Promise<number[]> {
    return [1,2,3,4,5,10000];
  }

  @IntentIgnoreBody()
  async getStringArray(): Promise<string[]> {
    return ["a", "b", "long string"];
  }

  @IntentIgnoreBody()
  async getObjectArray(): Promise<any[]> {
    return [{field:"value"}];
  }

  @IntentIgnoreBody()
  async getDecimalArray(): Promise<number[]> {
    return [1.5];
  }
}
