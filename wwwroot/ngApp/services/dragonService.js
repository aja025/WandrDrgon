class DragonService {
    constructor($resource) {
        this.dragonResource = $resource("/api/Dragon/:id",null,{"update":{method: "PUT"}});
        this.travelResource = $resource("/api/TravelLog/:id");
        this.itemLogResource = $resource("/api/ItemLog/:id",null,{"update":{method: "PUT"}});
        this.dragonSpriteResource = $resource("/api/DragonSprites/:id");        
        // this.imgResource = $resource("/api/dragonSprites/:id");
        // let actions = {
        //     "update": {method: "PUT", URL: "/api/Profile/:id"},
        // };
    }

    getDragon(id) {
        return this.dragonResource.get({ id: id });
    }
    getDragonSptite(id) {
        return this.dragonSpriteResource.get({ id: id });
    }

    getItemLog(id) {
        return this.itemLogResource.get({ id: id });
    }

    getDragons(){
        return this.dragonResource.query();
    }
    addDragon(dragon, callback) {
        this.dragonResource.save(dragon, callback);
    }
    addLog(travel) {
        this.travelResource.save(travel);
    }
    getLog(id)
    {
        return this.travelResource.query({id:id});
    }

    updateDragon(id,dragon){
        this.dragonResource.update({id:id},dragon);
    }

    updateItemLog(id,itemLog){
        this.itemLogResource.update({id:id}, itemLog);
    }

    getImg(dragonSpriteId) {
        return this.imgResource.get({ id: dragonSpriteId });
    }

    ageDragon(xp, dragonSpriteId, callback) {
        
        this.species = this.imgResource.get({ id: dragonSpriteId });
        this.heck = this.imgResource.get({ id: dragonSpriteId }).$promise.then((species) => {
            console.log(species.image3);
            if (xp < 10000) {
                console.log("success");
                return species.image1;
            }
            else if (xp >= 10000 && xp < 50000) {
                console.log("fail");
                return species.image2;
            }
            else if (xp >= 50000) {
                return species.image3;
            }
            
        });
    }

}