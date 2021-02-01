export class EmployeeListItem{
    constructor(
        public id: number,
        public firstName: string,
        public lastName: string,
        public departmentId: number | null | undefined,
    ) {}
}