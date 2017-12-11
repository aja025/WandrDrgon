class ProfileService {
    constructor($resource, $stateParams, $window, $q) {
        let actions = {
            "update": {
                method: "PUT",
                URL: "/api/Profile/:id"
            },
        };
        this.q = $q;
        this.window = $window;
        this.profileResource = $resource("/api/Profile/:id", null, actions);     //may need to add in :id
        this.friendResource = $resource("/api/friend");
    }

    // storeProfileInfo(profileInfo) {
    //  this.window.sessionStorage.setItem("profileId",  profileInfo.profileId);     
    // }

    getProfiles() {
        return this.profileResource.query();
    }

    getProfile(id) {
        return this.profileResource.get({ id: id });
    }
    getProfileId(userName) {
        return this.profileResource.get({ userName: userName }).profileId;
    }
    // getUser(){
    //     return this.user;
    // }
    addProfile(profile, callback) {
        this.profileResource.save(profile, callback);
    }
    editProfile(id, profile, callback) {
        this.profileResource.update({ id: id }, profile, callback);
    }
    editProfile2(id, profile) {
        this.profileResource.update({ id: id }, profile);
    }
    editProfileImage(id, imageURL) {
        console.log(imageURL);
        this.profileResource.update({ id: id }, {image: imageURL});
    }

    deleteProfile(id, callback) {
        this.profileResource.remove({ id: id }, callback);
    }
}