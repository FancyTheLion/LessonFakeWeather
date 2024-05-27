<script setup>
  import {defineExpose, defineProps, onMounted, ref} from "vue";
  import LoadingSymbol from "@/components/LoadingSymbol.vue";
  import {WebClientSendGetRequest} from "@/js/LibWebClient";
  import moment from "moment";

  const props = defineProps({
    weatherId: String
  })

  const isLoading = ref(true)
  const comments = ref([])

  defineExpose({
    ReloadComments
  })

  onMounted(async () =>
  {
    await OnLoad();
  })

  async function OnLoad()
  {
    await ReloadComments()

    isLoading.value = false
  }

  async function ReloadComments()
  {
    isLoading.value = true

    comments.value = (await (await WebClientSendGetRequest("/api/Comments/ByWeatherId/" + props.weatherId, {
      method: 'GET'
    })).json())
        .comments
        .sort(function(a, b) { return a.timestamp.localeCompare(b.timestamp) })

    isLoading.value = false
  }

</script>

<template>

  <LoadingSymbol
    v-if="isLoading" />

  <div
    v-if="!isLoading">

    <div
      v-if="comments.length === 0">
      Комментариев нет!
    </div>

    <div
      v-if="comments.length > 0"
      class="weather-comments-container">

      <div
          v-for="comment in comments" :key="comment.id"
          class="weather-comment-container">

        <div>

          <!-- First line - author and time -->
          <div
            class="weather-comment-top-line">

            <!-- Author -->
            <div>
              {{ comment.author.userName }}
            </div>

            <!-- Timestamp -->
            <div>
              {{ moment(comment.timestamp).format('DD.MM.YYYY HH:mm:ss') }}
            </div>

          </div>

          <!-- Second line - content -->
          <div>
            {{ comment.content }}
          </div>

        </div>

      </div>

    </div>

  </div>
</template>
