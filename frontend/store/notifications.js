export const state = () => ({
    notifications: [],
    newNotifications: false,
    openNotifications: false,
});

export const mutations = {
    deleteNotification(state, index){
        state.notifications.splice(index, 1);
    },
    setNotificationsModalState(state, action){
        state.openNotifications = action;
    },
    setNotifications(state, notifications){
        state.notifications = notifications;
        notifications.forEach(notif => {
            if(notif.wasSeen == false){
                state.newNotifications = true;
            }
        });
    },
    sawNotifications(state){
        for (let i = 0; i < state.notifications.length; i++) {
            state.notifications[i].wasSeen = true;
        }
        state.newNotifications = false;
    }
};

export const actions = {

};