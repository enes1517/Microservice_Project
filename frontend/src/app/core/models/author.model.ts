// Author Data Transfer Objects
export interface AuthorViewDto {
    id: string;
    fullName: string;
    phone: string;
    location?: string;
    bookCount: number;
}

export interface AuthorCreateDto {
    AuLname: string;
    AuFname: string;
    Phone: string;
    Address?: string;
    City?: string;
    Contract: boolean;
}

export interface UpdateAuthorDto {
    Id?: string;
    AuLname: string;
    AuFname: string;
    Phone: string;
    Address?: string;
    City?: string;
    Contract: boolean;
}
