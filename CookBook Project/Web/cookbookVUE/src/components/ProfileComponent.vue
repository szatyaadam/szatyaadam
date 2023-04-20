<template>
        <form class="form text-center d-flex flex-column flex-lg-row justify-content-center align-items-center" 
              @submit.prevent="modifyUser"
              id="profil-form">
            <div class=" w-100">
                <h1>Felhasználó</h1>
                <!-- USERNAME -->
                <label>Név</label>
                <div class="d-flex flex-lg-row justify-content-center align-items-center gap-1 my-1">
                    <input v-if="editName"
                        type="text" v-model="newUser.userName">
                    <span v-else
                          class="d-flex justify-content-between align-items-center">
                        {{ user.user.userName }}
                    </span>
                    <!-- EDIT BUTTON -->
                    <button v-if="!editName" 
                            type="button" 
                            @click.prevent="editName = !editName">
                        <EditIcon/>
                    </button>
                    <div v-else>
                        <!-- CLOSE EDIT BUTTON-->
                        <button type="button" 
                                @click.prevent="{editName = !editName};{newUser.userName = ''}">
                            <CloseIcon/>
                        </button>
                        <!-- SAVE BUTTON -->
                        <button v-if="newUser.userName.length >= 3"
                                type="submit">
                                <SaveIcon/>
                        </button>
                    </div>
                </div>
                <div class="message_space">
                    <!-- ERROR MESSAGE -->
                    <p class="error" v-if="newUser.userName.length < 3 && 
                        newUser.userName.length != 0">Minimum 3 karakter!</p>
                </div>
                <!-- EMAIL -->
                <label>Email</label>
                <div class="d-flex flex-lg-row justify-content-center align-items-center gap-1 my-1">
                    <input v-if="editEmail" 
                                 type="text" 
                                 v-model="newUser.email">
                    <span v-else 
                          class="d-flex justify-content-between align-items-center">
                        {{ user.user.email }}
                    </span>
                    <!-- EDIT BUTTON -->
                    <button v-if="!editEmail" 
                            type="button" 
                            @click.prevent="editEmail = !editEmail">
                        <EditIcon/>
                    </button>
                    <div v-else>
                        <!-- CLOSE EDIT BUTTON -->
                        <button type="button" 
                                @click.prevent="{editEmail = !editEmail};{newUser.email = ''}">
                            <CloseIcon/>    
                        </button>
                        <!-- SAVE BUTTON -->
                        <button v-if="regex.test(newUser.email)"
                                type="submit">
                            <SaveIcon/>    
                        </button>
                    </div>
                </div>
                <div class="message_space">
                    <!-- ERROR MESSAGE -->
                    <p class="error" v-if="!regex.test(newUser.email) &&
                        newUser.email.length != 0">Email cím formátuma nem megfelelő!</p>
                </div>
                <!-- PASSWORD -->
                <div class="d-flex flex-column flex-lg-row justify-content-center align-items-center gap-3 my-2">
                    <label v-if="!editPassword">Jelszó</label>
                    <div v-if="editPassword">

                        <p>Jelszó</p>
                        <input type="password" 
                               v-model="newUser.password">

                        <p>Jelszó megerősítése</p>
                        <input type="password" 
                               v-model="passwordConfirm">
                    </div>
                    <!-- EDIT BUTTON -->
                    <button v-if="!editPassword" 
                            type="button" 
                            @click.prevent="editPassword = !editPassword">
                        <EditIcon/>
                    </button>
                    <div v-else>
                        <!-- CLOSE EDIT BUTTON -->
                        <button type="button" 
                                @click.prevent="{editPassword = !editPassword};{newUser.password = ''}">
                            <CloseIcon/>
                        </button>
                        <!-- SAVE BUTTON -->
                        <button v-if="newUser.password == passwordConfirm &&
                                      newUser.password.length > 7" 
                                      type="submit">
                            <SaveIcon/>
                        </button>
                    </div>
                </div>
                <!-- NOTIFY USER -->
                <div>
                    <!-- PASSWORD CONFIRM ERROR MESSAGE -->
                    <p v-if="newUser.password != passwordConfirm" 
                            class="error">A jelszó nem egyezik!</p>
                    <!-- PASSWORD LENGHT CHECK -->
                    <p v-if="newUser.password.length <= 7 &&
                             newUser.password.length != 0" 
                             class="error">Minimum 8 karakter hosszúnak kell lenni.</p>
                    <!--SERVER ERROR MESSAGE -->
                    <p v-if="user.errorMessage" 
                             class="error">{{ user.errorMessage }}</p>
                    <!-- NOTIFY MESSAGE -->
                    <p v-if="user.notifyMessage" 
                             class="success" >{{ user.notifyMessage }}</p>
                </div>
            </div>
        </form>
</template>

<script setup>
import EditIcon from '../assets/icon/EditIcon.vue';
import CloseIcon from '../assets/icon/CloseIcon.vue';
import SaveIcon from '../assets/icon/SaveIcon.vue';
import { useUserStore } from '../stores/user';
import { ref, watch, reactive } from 'vue';
import axios from 'axios';
import base from '../service/urlBase';

const user = useUserStore()
const newUser = reactive({
    userName: '',
    email: '',
    password: ''
})

// //source : https://uibakery.io/regex-library/email
const regex = /^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$/

const editName = ref(false)
const editEmail = ref(false)
const editPassword = ref(false)
const passwordConfirm = ref('')

watch(editPassword, () => {
    if(editPassword){
        newUser.password = ''
        passwordConfirm.value = ''
    }
})

//USER MODIFY
const modifyUser = async () => {
    const data = {
      userName : newUser.userName,
      email: newUser.email,
      password: newUser.password
    }

    await axios.put(base.userUrl + '/modify', data , user.config())
    .then(res => {
        if(newUser.userName != ''){
            user.user.userName = newUser.userName
        }

        if (newUser.email != ''){
            user.user.email = newUser.email
        }

        editName.value = false
        editEmail.value = false
        editPassword.value = false
        user.createNotifyMessage(res.data)
    })
    .catch(err => {
        newUser.userName = user.user.userName
        newUser.email = user.user.email
        editName.value = false
        editEmail.value = false
        editPassword.value = false
        user.createErrorMessage(err.response.data);
    })
}
</script>

<style scoped>
#profil-form{
    width: 500px;
    height: 500px;
    max-width: 100vw;
    max-height: 100vh;
    background-color: whitesmoke;
    border: 5px solid rgb(0, 0, 0);
    border-radius: 5px;
    padding: 20px;
}

button:not(.btn){
    background-color: transparent;
    border-color: transparent;
}

svg{
    height: 20px;
    aspect-ratio: 1;
}

span{
    width: 80%;
    height: 30px;
    max-width: 350px;
    background-color: rgba(42, 42, 42, 0.963);
    color: white;
    border-radius: 20px;
    text-indent: 20px;
}

input{
    height: 30px;
    width: 75%;
    max-width: 330px;
    min-width: 200px;
    background-color: rgba(222, 238, 251, 0.276);
    border-radius: 20px;
    text-indent: 10px;
}

.message_space{
    height: 20px;
}
</style>
