// Author Data Transfer Objects
export interface AuthorViewDto {
    id: string;
    fullName: string;
    phone: string;
    location?: string;
    bookCount: number;
}

export interface AuthorCreateDto {
    auLname: string;
    auFname: string;
    phone: string;
    address?: string;
    city?: string;
    contract: boolean;
}

export interface UpdateAuthorDto {
    id?: string;
    auLname: string;
    auFname: string;
    phone: string;
    address?: string;
    city?: string;
    contract: boolean;
}
