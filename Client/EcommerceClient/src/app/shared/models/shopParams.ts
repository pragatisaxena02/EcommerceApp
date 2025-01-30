export class ShopParams
{
    brands : string[] = [];
    types : string[] = [];
    search?: string;
    sort = 'name';
    pageNumber: number = 0;
    pageSize :number =  6;
}