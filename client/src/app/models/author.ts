export interface Author {
    id: number;
    fullName: string;
    country: string;
    publicId: string | null;
    imageUrl: string | null;
}

export interface DetailAuthor extends Author {
    biography: string;
}
