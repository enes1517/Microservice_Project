// Product (Title/Book) Data Transfer Objects
// Backend TitleDto'ya tam uyumlu - küçük harf field'lar
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

export interface UpdateTitleDto {
    id: string;
    title: string;
    price?: number;
    type: string;
    notes?: string;
    royalty?: number;
}
