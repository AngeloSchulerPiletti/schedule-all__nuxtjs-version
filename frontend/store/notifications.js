export const state = () => ({
    notifications: [],
    newNotifications: false,
});

export const mutations = {
    setNotifications(state, notifications){
        state.notifications = notifications;
        notifications.forEach(notif => {
            if(notif.wasSeen == false){
                state.newNotifications = true;
            }
        });
    }
};

export const actions = {

};