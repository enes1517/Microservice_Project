export interface ApiResponse<T> {
    data?: T;
    message?: string;
    isSuccess: boolean;
}

export interface PaginationParams {
    pageSize: number;
    pageIndex: number;
}
