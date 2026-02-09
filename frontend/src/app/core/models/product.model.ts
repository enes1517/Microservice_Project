// Product (Title/Book) Data Transfer Objects
export interface TitleDto {
    id: string;
    title: string;
    price?: number;
    type: string;
    notes?: string;
    royalty?: number;
}

export interface CreateTitleDto {
    title: string;
    price?: number;
    type: string;
    notes?: string;
    royalty?: number;
    pubdate: Date | string;
}

export interface UpdateTitleDto extends TitleDto {
    // Inherits all properties from TitleDto
}
