<template>
  <section class="box section">
    <h2 class="is-size-3">Check-In</h2>
    <hr />
    <div class="columns">
      <div class="column">
        <b-field label="Name">
          <b-select
            required
            v-model="participantLog.participantId"
            placeholder="Please select a name..."
            type="is-danger"
            message="Name is required."
          >
            <option
              v-for="participant in participants"
              :key="participant.id"
              :value="participant.id"
            >{{participant.name}}</option>
          </b-select>
        </b-field>
        <b-field label="Current Weight">
          <b-input
            placeholder="150.0"
            min="0"
            max="999"
            v-model.number="participantLog.currentWeight"
            required
          ></b-input>
        </b-field>
      </div>
      <div class="column has-text-centered">
        <b-field>
          <b-upload v-model="uploadedPhoto" drag-drop>
            <section class="section has-text-centered">
              <p>
                <b-icon icon="upload" size="is-large"></b-icon>
              </p>
              <p>Drop your file here or click to upload!</p>
            </section>
          </b-upload>
        </b-field>
        <div class="container">
          <div class="tags is-centered">
            <span v-if="uploadedPhoto.name.length > 0" class="tag is-primary">
              {{uploadedPhoto.name}}
              <button
                @click="removeFile()"
                type="button"
                class="delete is-small"
              ></button>
            </span>
          </div>
        </div>
      </div>
    </div>
    <div>
      <div class="buttons is-pulled-right">
        <button
          :disabled="participantLog.participantId == undefined || participantLog.currentWeight == undefined"
          @click="submit"
          class="button is-primary is-outlined"
        >
          <span class="icon">
            <i class="fas fa-save"></i>
          </span>
          <span>Submit</span>
        </button>
        <button @click="$parent.close()" class="button is-danger is-outlined">
          <span class="icon">
            <i class="fas fa-ban"></i>
          </span>
          <span>Close</span>
        </button>
      </div>
    </div>
    <b-loading :active.sync="isLoading"></b-loading>
  </section>
</template>
<script lang="ts">
import Vue from 'vue';
import { Prop, Component } from 'vue-property-decorator';
import Participant from '../models/participant.model';
import ParticipantLog from '../models/participant-log.model';
import ParticipantLogsService from '@/services/leaderboard/participant-logs.service';
import CheckInModel from '@/models/check-in.model';
@Component
export default class CheckIn extends Vue {
  private service = new ParticipantLogsService();

  @Prop({ default: 0 })
  public healthInititativeId!: number;
  @Prop({ default: () => new Array<Participant>() })
  public participants!: Participant[];
  public participantLog = new ParticipantLog();
  public uploadedPhoto = new File([], '');
  public isLoading = false;

  public async submit() {
    this.isLoading = true;
    try {
      const model = new CheckInModel();
      model.participantLog = this.participantLog;
      model.healthInitiativeId = this.healthInititativeId;
      if (this.uploadedPhoto.size > 0) {
        const base64Photo = await this.toBase64(this.uploadedPhoto)
        model.photoBase64 = (base64Photo as string).split(',')[1];
      }

      await this.service.saveLog(model);
      this.$buefy.notification.open({
        duration: 5000,
        message: 'Check-in saved successfully!',
        type: 'is-success',
        hasIcon: true,
        position: 'is-bottom-right'
      });
      // eslint-disable-next-line @typescript-eslint/no-explicit-any
      (this.$parent as any).close();
      this.$emit('checkin-success');
    } catch (error) {
      if (error.response.status === 400) {
        this.$buefy.notification.open({
          duration: 5000,
          message: error.response.data,
          type: 'is-danger',
          hasIcon: true,
          position: 'is-bottom-right'
        });
      } else {
        this.$buefy.notification.open({
          duration: 5000,
          message: 'An error has ocurred while attempting to create the your check-in!',
          type: 'is-danger',
          hasIcon: true,
          position: 'is-bottom-right'
        });
      }
    }
    this.isLoading = false;
  }
  public removeFile() {
    this.uploadedPhoto = new File([], '');
  }
  private toBase64 = (file: File) => new Promise((resolve, reject) => {
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => resolve(reader.result);
    reader.onerror = error => reject(error);
  });
}

</script>
