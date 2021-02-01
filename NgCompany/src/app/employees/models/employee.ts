export class Employee{
    constructor(
        public id: number,
        public firstName: string,
        public lastName: string,
        public middleName: string,
        public position: string,
        public createdAt: string,
        public changedAt: string,
        public hiredAt: string,
        public departmentId: number | null | undefined
    ) {}
}