import { Entity, PrimaryGeneratedColumn, Column } from 'typeorm';

@Entity('tpt_fk_base_class')
export class TptFkBaseClass {
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