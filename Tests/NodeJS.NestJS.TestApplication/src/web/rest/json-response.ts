export class JsonResponse<T> {
    constructor(value : T) {
        this.value = value;
    }

    value: T;
}