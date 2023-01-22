// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "CoreMinimal.h"
#include "Components/ActorComponent.h"
#include "FlashLightComponent.generated.h"


UCLASS( ClassGroup=(Custom), meta=(BlueprintSpawnableComponent) )
class PROJECT_API UFlashLightComponent : public UActorComponent
{
	GENERATED_BODY()

public:	
	// Sets default values for this component's properties
	UFlashLightComponent();

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "FlashLight")
		float MaxBatteryLevel{ 100.f };
	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "FlashLight")
		float MinBatteryLevel{ .0f };
	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "FlashLight")
		float CurrentBatteryLevel{ 50.f };
	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "FlashLight")
		float DepletionAmount{ 1.f };
	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "FlashLight")
		float DepletionSpeed{ .1f };
	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "FlashLight")
		float MaxIntensity{ 1.f };
	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "FlashLight")
		float FadeAtPercent{ 20.f };

protected:
	// Called when the game starts
	virtual void BeginPlay() override;
	
	class AActor* m_Player{};
	class ULightComponent* m_Light{};

	// Timer handle
	FTimerHandle m_TimerHandle{};

public:	
	// Called every frame
	virtual void TickComponent(float DeltaTime, ELevelTick TickType, FActorComponentTickFunction* ThisTickFunction) override;

	// Initialize function that takes a L1_Character object reference and sets it to Player
	UFUNCTION(BlueprintCallable, Category = "FlashLight")
		void Initialize(class AActor* Player);

	UFUNCTION(BlueprintCallable, Category = "FlashLight")
		void ToggleFlashLight();
	UFUNCTION(BlueprintCallable, Category = "FlashLight")
		void IncreaseBattery(float Amount);
	UFUNCTION(BlueprintCallable, Category = "FlashLight")
		void DecreaseBattery();
	UFUNCTION(BlueprintCallable, Category = "FlashLight")
		void SetIntensity();
};
