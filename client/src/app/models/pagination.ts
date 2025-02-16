export interface FilterParams {
    pageIndex: number;
    pageSize: number;
    sort: string;
}
export interface MetaData {
    pageIndex: number;
    totalPages: number;
    pageSize: number;
    totalCount: number;
}

export class PaginatedResponse<T> {
    items: T;
    metaData: MetaData;

    constructor(items: T, metaData: MetaData) {
        this.items = items;
        this.metaData = metaData;
    }
}
