module app.common.services {

    interface IConstant {
        apiProductURI: string;
        apiPostURI: string;
    }

    export class ConstantService implements IConstant {
        apiPostURI: string;
        apiProductURI: string;

        constructor() {
            this.apiPostURI = '/api/posts/';
            this.apiProductURI = 'http://api.regalodecasamiento.com/api/product/';
        }
    }

    angular.module('chsakellBlogApp')
        .service('constantService', ConstantService);
}