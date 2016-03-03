module app.domain {
    export interface IProduct {
        ID?: number;
        ProductName: string;
        ProductDescription: string;
        ProductPrice: number;
        MainImage: string; 
    }

    export class Product extends app.domain.EntityBase implements IProduct {
        constructor(public ProductName: string,
            public ProductDescription: string,
            public ProductPrice: number,
            public MainImage: string,
            public ID?: number) {

            super();

            this.ID = ID;
            this.ProductName = ProductName;
            this.ProductDescription = ProductDescription;
            this.ProductPrice = ProductPrice;
            this.MainImage = MainImage;            
        }
    }
}