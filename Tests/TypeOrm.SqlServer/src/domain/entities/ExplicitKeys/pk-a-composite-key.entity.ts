import { Entity, PrimaryGeneratedColumn, Column } from 'typeorm';

@Entity('pk_a_composite_key')
export class PK_A_CompositeKey {
  @PrimaryGeneratedColumn('uuid')
  compositeKeyA: string;

  @PrimaryGeneratedColumn('uuid')
  compositeKeyB: string;

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}