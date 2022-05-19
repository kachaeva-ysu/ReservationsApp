export default class ClientError extends Error {
    constructor() {
        super();
        this.name = 'ClientError';
    }
}