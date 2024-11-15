export interface PaginationParams {
    pageIndex: number;
    pageSize: number;
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
