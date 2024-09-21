export class LoginRequest {
    UserName!: string | null;
    Password!: string | null;
    constructor(userName: string, password : string) {
        this.UserName = userName;
        this.Password = password;
    }
}
