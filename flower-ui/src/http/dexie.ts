import Dexie from 'dexie';

class MyDatabase extends Dexie{
    constructor(databaseName: string){
        super(databaseName);
        this.version(1).stores({});
        this.createTable = this.createTable.bind(this);
    }

    createTable<T>(tableName: string){
        this.version(1).stores({
            [tableName]: '++id,name,messages,boolkey',
        });

        return this.table<T,number>(tableName);
    }
}


// class MyDatabase extends Dexie { //定义了一个名为 MyDatabase 的类，它继承自 Dexie 类，用于创建 IndexedDB 数据库。
//   items: Dexie.Table<Item, number>; // 表示 Item 表的数据类型和主键类型

//   constructor() {
//     super('MyDatabase'); //调用了 Dexie 类的构造函数，并传入了数据库的名称 'MyDatabase'。它初始化了父类的功能。

//     // 定义数据库的版本和表结构
//     this.version(1).stores({
//       items: '++id,name,description', // 表示 Item 表的结构，其中 id 是自增主键
//     });

//     // 获取 items 表的实例
//     this.items = this.table('items');
//   }
// }
// interface Item {
//   id?: number;
//   name: string;
//   description: string;
// }

export const db = new MyDatabase('MyDatabase');