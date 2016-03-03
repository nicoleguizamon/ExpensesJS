module app.productList {

    interface IProductsViewModel {
        products: app.domain.IProduct[];
        remove(Id: number): void;
    }

    class ProductsCtrl implements IProductsViewModel {
        products: app.domain.IProduct[];

        static $inject = ['constantService', 'dataService'];
        constructor(private constantService: app.common.services.ConstantService,
            private dataService: app.common.services.DataService) {
            this.getProducts();
        }

        remove(Id: number): void {
            var self = this; // Attention here.. check 'this' in TypeScript and JavaScript

            this.dataService.remove(this.constantService.apiProductURI + Id)
                .then(function (result) {
                    self.getProducts();
                });
        }

        getProducts(): void {
            this.dataService.get(this.constantService.apiProductURI).then((result: app.domain.IProduct[]) => {
                this.products = result;
            });
        }
    }
    angular.module('chsakellBlogApp')
        .controller('ProductsCtrl', ProductsCtrl);
}